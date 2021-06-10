<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

Route::middleware('auth:api')->get('/user', function (Request $request) {
    return $request->user();
});


Route::get('/test', function(){
    $response = new \Illuminate\Http\Response(json_encode(['msg' => 'está funcionando camarada!']));
    $response->header('Content-Type', 'application/json');

    return $response;

});

Route::namespace('/administrativo', function () {
    return App\Models\Administrativo::all(); 
});

Route::get('/cfc', function () {
    return App\Models\Cfc::all(); 
});

Route::get('/edriving', function () {
    return App\Models\Edriving::all(); 
});

Route::get('/parceiro', function () {
    return App\Models\Parceiro::all(); 
});

Route::get('/usuario', function () {
    return App\Models\Usuario::all(); 
});



