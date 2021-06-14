<?php

namespace App\Http\Controllers\Api;
use App\Models\Usuario;
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
        $usr = $this->usuario->all();

        return response()->json($usr);
    }

    public function show($id)
    {
        return Usuario::where('idUsuario', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $usr = $this->usuario->create($data);
        return response()->json($usr);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Usuario::where('idUsuario', $id)->update($data);

    }
}
