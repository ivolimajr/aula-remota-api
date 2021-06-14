<?php

namespace App\Http\Controllers\Api;
use App\Models\Instrutor;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class InstrutorController extends Controller
{
    private $instrutor;

    public function __construct(Instrutor $instrutor)
    {
        $this->instrutor = $instrutor;
    }

    public function index()
    {
        $instr = $this->instrutor->all();

        return response()->json($instr);
    }

    public function show($id)
    {
        return Instrutor::where('idInstrutores', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $instr = $this->instrutor->create($data);
        return response()->json($instr);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Instrutor::where('idInstrutores', $id)->update($data);

    }
}
