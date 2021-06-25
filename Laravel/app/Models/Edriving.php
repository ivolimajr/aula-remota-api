<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Edriving extends Model
{
    protected $fillable = [
        'fullName','email', 'cpf', 'telefone', 'status', 'cargo'
    ];
}
