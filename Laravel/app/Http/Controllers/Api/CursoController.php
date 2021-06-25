<?php

namespace App\Http\Controllers\Api;
use App\Models\Curso;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class CursoController extends Controller
{
    private $curso;

    public function __construct(Curso $curso)
    {
        $this->curso = $curso;
    }

    public function index()
    {
        $curs = $this->curso->all();

        return response()->json($curs);
    }

    public function show($id)
    {
        return Curso::where('idCursos', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $curs = $this->curso->create($data);
        return response()->json($curs);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Curso::where('idCursos', $id)->update($data);

    }

}
