<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Usuario extends Model
{
    protected $fillable = [
        'fullName',
        'telefone',
        'email', 
        'senha',
        'status', // 0 => DELETADO | 1=> ATIVO | 2 => OCULTO
        'nivelAcesso'  //10 a 19 => Plataforma | 20 a 29 => PARCEIRO | 30 A 39 => CFC
    ];
}
