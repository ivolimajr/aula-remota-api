using AulaRemota.Core.Arquivo.Upload;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.Auto_Escola;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.AutoEscola.Criar
{
    public class AutoEscolaCriarHandler : IRequestHandler<AutoEscolaCriarInput, AutoEscolaCriarResponse>
    {
        private readonly IRepository<AutoEscolaModel> _autoEscolaRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<AutoEscolaCargoModel> _cargoRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public AutoEscolaCriarHandler(
            IRepository<AutoEscolaModel> autoEscolaRepository,
            IRepository<UsuarioModel> usuarioRepository,
            IRepository<AutoEscolaCargoModel> cargoRepository,
            IRepository<TelefoneModel> telefoneRepository,
            IRepository<ArquivoModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }
        public async Task<AutoEscolaCriarResponse> Handle(AutoEscolaCriarInput request, CancellationToken cancellationToken)
        {
            try
            {
                _autoEscolaRepository.CreateTransaction();

                var arquivoResult = await _mediator.Send(new ArquivoUploadInput { Arquivo = request.Arquivo });
                //VERIFICA SE O EMAIL JÁ ESTÁ EM USO
                var emailResult = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                if (emailResult != null) throw new HttpClientCustomException("Email já em uso");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                var cpfResult = await _autoEscolaRepository.FindAsync(u => u.Cnpj == request.Cnpj);
                if (cpfResult != null) throw new HttpClientCustomException("Cnpj já existe em nossa base de dados");

                //VERIFICA SE O CPF JÁ ESTÁ EM USO
                foreach (var item in request.Telefones)
                {
                    var telefoneResult = await _telefoneRepository.FindAsync(u => u.Telefone == item.Telefone);
                    if (telefoneResult != null) throw new HttpClientCustomException("Telefone: " + telefoneResult.Telefone + " já em uso");
                }

                //VERIFICA SE O CARGO INFORMADO EXISTE
                var cargo = _cargoRepository.GetById(request.CargoId);
                if (cargo == null) throw new HttpClientCustomException("Cargo informado não existe");

                //CRIA UM USUÁRIO
                var user = new UsuarioModel()
                {
                    Nome = request.RazaoSocial.ToUpper(),
                    Email = request.Email.ToUpper(),
                    NivelAcesso = 20,
                    status = 1,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                };

                //CRIA UM ENDEREÇO
                var endereco = new EnderecoModel()
                {
                    Bairro = request.Bairro.ToUpper(),
                    Cep = request.Cep.ToUpper(),
                    Cidade = request.Cidade.ToUpper(),
                    EnderecoLogradouro = request.EnderecoLogradouro.ToUpper(),
                    Numero = request.Numero.ToUpper(),
                    Uf = request.Uf.ToUpper(),
                };

                var arquivo = await _arquivoRepository.CreateAsync(arquivoResult);
                await _arquivoRepository.SaveChangesAsync();

                //CRIA UM EDRIVING
                var autoEscola = new AutoEscolaModel()
                {
                    RazaoSocial = request.RazaoSocial.ToUpper(),
                    Cnpj = request.Cnpj.ToUpper(),
                    Email = request.Email.ToUpper(),
                    CargoId = request.CargoId,
                    Telefones = request.Telefones,
                    DataFundacao = request.DataFundacao,
                    InscricaoEstadual = request.InscricaoEstadual,
                    NomeFantasia = request.NomeFantasia,
                    Site = request.Site,
                    Cargo = cargo,
                    Usuario = user,
                    Endereco = endereco,
                    Arquivos = new List<ArquivoModel>() { arquivo }
                };


                var autoEscolaModel = await _autoEscolaRepository.CreateAsync(autoEscola);

                //await _mediator.Send(new EnviarEmailRegistroInput { Para = request.Email, Senha = request.Senha });

                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return new AutoEscolaCriarResponse()
                {
                    Id = autoEscolaModel.Id,
                    RazaoSocial = autoEscolaModel.RazaoSocial.ToUpper(),
                    Cnpj = autoEscolaModel.Cnpj.ToUpper(),
                    Email = autoEscolaModel.Email.ToUpper(),
                    Descricao = autoEscolaModel.Descricao,
                    CargoId = autoEscolaModel.CargoId,
                    Telefones = autoEscolaModel.Telefones,
                    DataFundacao = autoEscolaModel.DataFundacao,
                    InscricaoEstadual = autoEscolaModel.InscricaoEstadual,
                    NomeFantasia = autoEscolaModel.NomeFantasia,
                    Site = autoEscolaModel.Site,
                    Cargo = autoEscolaModel.Cargo,
                    Usuario = autoEscolaModel.Usuario,
                    Endereco = autoEscolaModel.Endereco,
                };
            }
            catch (Exception e)
            {
                _autoEscolaRepository.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                _autoEscolaRepository.Context.Dispose();
            }
        }
    }
}
