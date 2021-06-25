<?php

namespace App\Http\Controllers\Api;
use App\Models\Edriving;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class EdrivingController extends Controller
{
    private $edriving;

    public function __construct(Edriving $edriving)
    {
        $this->edriving = $edriving;
    }

    public function index()
    {
        $edr = $this->edriving->all();

        return response()->json($edr);
    }

    public function show($id)
    {
        return Edriving::where('idEdriving', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $Edriving = $this->edriving->create($data);
        return response()->json($Edriving);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Edriving::where('idEdriving', $id)->update($data);

    }
}
