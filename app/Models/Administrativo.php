<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Administrativo extends Model
{
    protected $fillable = [
        'fullName','email','cpf','identidade', 'telefone',
        'status', 'cargo','cep', 'bairro', 'cidade','uf', 'numero',
        'dataNascimento','enderecoLogradouro','localizacaoLatitude',
        'longitude', 'orgaoexpedidor', 'site'
        
    ];
}
