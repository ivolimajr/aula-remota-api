<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Cfc extends Model
{
    protected $fillable = [
        'razaoSocial','nomeFantasia','cnpj','inscricaoEstadual',
        'datadaFundacao','enderecoLogradouro','numero','bairro',
        'cidade','uf','cep','localizacaoLatitude','longitude',
        'telefone1','telefone2','email','site',
    ];
}
