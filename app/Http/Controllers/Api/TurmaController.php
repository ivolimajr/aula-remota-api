<?php

namespace App\Http\Controllers\Api;
use App\Models\Turma;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class TurmaController extends Controller
{
    private $turma;

    public function __construct(Turma $turma)
    {
        $this->turma = $turma;
    }

    public function index()
    {
        $turm = $this->turma->all();

        return response()->json($turm);
    }

    public function show($id)
    {
        return Turma::where('idTurmas', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $turm = $this->turma->create($data);
        return response()->json($turm);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Turma::where('idTurmas', $id)->update($data);
    }
}
