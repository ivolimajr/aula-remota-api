<?php

namespace App\Http\Controllers\Api;
use App\Models\Parceiro;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class ParceiroController extends Controller
{
    private $parceiro;

    public function __construct(Parceiro $parceiro)
    {
        $this->parceiro = $parceiro;
    }

    public function index()
    {
        $parc = $this->parceiro->all();

        return response()->json($parc);
    }

    public function show($id)
    {
        return Parceiro::where('idParceiro', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $parc = $this->parceiro->create($data);
        return response()->json($parc);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Parceiro::where('idParceiro', $id)->update($data);

    }
}
