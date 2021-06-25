<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Cfc extends Model
{
    protected $fillable = [
        'fullName', 'email', 'telefone', 'status', 'bairro',
        'cep', 'cidade', 'cnpj', 'datadaFundacao', 'enderecoLogradouro',
        'inscricaoEstadual', 'localizacaoLatitude', 'longitude',
        'nomeFantasia', 'numero', 'razaoSocial', 'site', 'uf'

    ];
}
