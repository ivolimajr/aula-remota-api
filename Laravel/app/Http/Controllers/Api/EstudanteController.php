<?php

namespace App\Http\Controllers\Api;
use App\Models\Estudante;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class EstudanteController extends Controller
{
    private $estudante;

    public function __construct(Estudante $estudante)
    {
        $this->estudante = $estudante;
    }

    public function index()
    {
        $est = $this->estudante->all();

        return response()->json($est);
    }

    public function show($id)
    {
        return Estudante::where('idEstudantes', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $est = $this->estudante->create($data);
        return response()->json($est);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Estudante::where('idEstudantes', $id)->update($data);

    }
}
