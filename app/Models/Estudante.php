<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Estudante extends Model
{
    protected $fillable = [
        'nome', 'cpf','identidade',
        'orgaoexpedidor','dataNascimento','enderecoLogradouro',
        'numero','bairro','cidade','uf','cep','email',
        'telefone1','telefone2','curso', 'turno', 'turma'
    ];
}
