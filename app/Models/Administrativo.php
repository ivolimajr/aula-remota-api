<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Administrativo extends Model
{
    protected $fillable = [
        'nome','email','cargo','cpf','identidade',
        'orgaoexpedidor','dataNascimento','enderecoLogradouro',
        'numero','bairro','cidade','uf','cep','localizacaoLatitude',
        'longitude','telefone1','telefone2','email','site',
    ];
}
