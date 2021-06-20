<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Estudante extends Model
{
    protected $fillable = [
        'fullName', 'email', 'cpf','identidade', 'telefone', 'turma',
        'cep', 'cidade', 'bairro', 'uf', 'enderecoLogradouro', 
        'numero', 'curso', 'dataNascimento', 'identidade', 'orgaoexpedidor',
        'turno', 'turma'
    ];
}
