<?php

namespace App\Http\Controllers\Api;
use App\Models\Cfc;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class CfcController extends Controller
{
    private $cfc;

    public function __construct(Cfc $cfc)
    {
        $this->cfc = $cfc;
    }

    public function index()
    {
        $centrofc = $this->cfc->all();
        
        return response()->json($centrofc);
    }

    public function show($id)
    {
        return Cfc::where('idCfc', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $cfc = $this->cfc->create($data);
        return response()->json($cfc);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Cfc::where('idCfc', $id)->update($data);

    }
}
