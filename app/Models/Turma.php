<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Turma extends Model
{
    protected $fillable = [
        'cursoId','instrutorId', 'turma', 'alunos', 
        'link', 'dataInicio', 'dataFim', 'matricula'
    ];
}
