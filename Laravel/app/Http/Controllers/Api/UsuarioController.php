<?php

namespace App\Http\Controllers\Api;
use App\Models\Usuario;
use App\Http\Controllers\Controller;
use App\Http\Requests\UsuarioRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Hash;
use Illuminate\Validation\ValidationException;

class UsuarioController extends Controller
{
    private $usuario;

    public function __construct(Usuario $usuario)
    {
        $this->usuario = $usuario;
    }

    /**
     * BUSCA TODOS OS USUÁRIO NO BANCO, SE TIVER UMA FALHA NA CUNSULTA O SISTEMA RETORNA UMA EXCEPTION
     */
    public function index()
    {
        $result = $this->usuario->all();
        if($result) return $result;
        if(!$result) return  response()->json(['result' => 'internal server error'], 500);
    }

    public function show($id)   
    {
        $result = Usuario::where('idUsuario', $id)->first();
        return $result;
        if(!$result) return  response()->json(['result' => 'not found'], 200);
    }

    /**
    * 
    */
    public function save(Request $request)
    {
        if(Usuario::where('email', $request->email)->first()) return  response()->json(['result' => 'email already exists'], 202);

        $request->merge(['senha' => Hash::make($request->senha)]);
        $result = $this->usuario->create($request->all());
        return $result;
        if(!$result) return response()->json(['result' => 'internal server error'], 500);

    }

    public function login(UsuarioRequest $request)
    {
        $result = Usuario::where('email', $request->email)->first();
        if(!$result) return  response()->json(['result' => 'not Found'], 404);

        if (Hash::check($request->senha, $result->senha)){
            return $result;
        } else{
            return  response()->json(['result' => 'senha incorreta'], 202);
        }        
    }

    public function update(Request $request, $id)
    {
        $result = Usuario::where('idUsuario', $id)->update($request->all());
        return  $result;
        if(!$result) return  response()->json(['result' => 'not found'], 200);
    }
}
