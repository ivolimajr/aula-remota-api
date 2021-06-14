<?php

namespace App\Http\Controllers\Api;
use App\Models\Administrativo;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;

class AdministrativoController extends Controller
{
    private $administrativo;

    public function __construct(Administrativo $administrativo)
    {
        $this->administrativo = $administrativo;
    }

    public function index()
    {
        $adm = $this->administrativo->all();

        return response()->json($adm);
    }

    public function show($id)
    {
        return Administrativo::where('idAdministrativo', $id)->get();
    }

    public function save(Request $request)
    {
        $data = $request->all();
        $adm = $this->administrativo->create($data);
        return response()->json($adm);
    }

    public function update(Request $request, $id)
    {
        $data = $request->all();

        return Administrativo::where('idAdministrativo', $id)->update($data);

    }
}
