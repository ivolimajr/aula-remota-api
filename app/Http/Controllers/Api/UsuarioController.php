<?php

namespace App\Http\Controllers\Api;

use App\Model\Usuario;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class UsuarioController extends Controller
{
    private $usuario;

    public function __construct(Usuario $usuario)
    {
        $this->usuario = $usuario;
    }

    public function index()
    {
        $usuario = $this->usuario->all();

        return response()->json($usuario);
    }
}
