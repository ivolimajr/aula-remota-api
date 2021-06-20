<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Parceiro extends Model
{
    protected $fillable = [
        'fullName','email','status', 'telefone', 'cep','cnpj', 'cargo',
        'bairro', 'cidade', 'descricao', 'enderecoLogradouro', 'numero', 'uf',
    ];
}
