<?php

use App\Http\Controllers\Api\AdministrativoController;
use App\Http\Controllers\Api\CfcController;
use App\Http\Controllers\Api\EdrivingController;
use App\Http\Controllers\Api\UsuarioController;
use App\Http\Controllers\Api\InstrutorController;
use App\Http\Controllers\Api\EstudanteController;
use App\Http\Controllers\Api\ParceiroController;
use App\Http\Controllers\Api\TurmaController;
use App\Http\Controllers\Api\CursoController;
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

Route::get('/test', function() {
    $response = new \Illuminate\Http\Response(json_encode(['msg' => 'está funcionando camarada!']));
    $response->header('Content-Type', 'application/json');

    return $response;
});

Route::namespace('Api')->group(function () {
 
    Route::prefix('administrativo')->group(function () {
        Route::get('/', [AdministrativoController::class, 'index']);
        Route::get('/{id}', [AdministrativoController::class, 'show']);
        Route::post('/', [AdministrativoController::class, 'save']);
        Route::put('/{id}', [AdministrativoController::class, 'update']);
    });

    Route::prefix('cfc')->group(function () {
        Route::get('/', [CfcController::class, 'index']);
        Route::get('/{id}', [CfcController::class, 'show']);
        Route::post('/', [CfcController::class, 'save']);
        Route::put('/{id}', [CfcController::class, 'update']);
    });

    Route::prefix('edriving')->group(function () {
        Route::get('/', [EdrivingController::class, 'index']);
        Route::get('/{id}', [EdrivingController::class, 'show']);
        Route::post('/', [EdrivingController::class, 'save']);
        Route::put('/{id}', [EdrivingController::class, 'update']);
    });

    Route::prefix('parceiro')->group(function () {
        Route::get('/', [ParceiroController::class, 'index']);
        Route::get('/{id}', [ParceiroController::class, 'show']);
        Route::post('/', [ParceiroController::class, 'save']);
        Route::put('/{id}', [ParceiroController::class, 'update']);
    });

    Route::prefix('usuario')->group(function () {
        Route::get('/', [UsuarioController::class, 'index']);
        Route::get('/{id}', [UsuarioController::class, 'show']);
        Route::post('/', [UsuarioController::class, 'save']);
        Route::put('/{id}', [UsuarioController::class, 'update']);
    });

    Route::prefix('instrutor')->group(function () {
        Route::get('/', [InstrutorController::class, 'index']);
        Route::get('/{id}', [InstrutorController::class, 'show']);
        Route::post('/', [InstrutorController::class, 'save']);
        Route::put('/{id}', [InstrutorController::class, 'update']);
    });

    Route::prefix('estudante')->group(function () {
        Route::get('/', [EstudanteController::class, 'index']);
        Route::get('/{id}', [EstudanteController::class, 'show']);
        Route::post('/', [EstudanteController::class, 'save']);
        Route::put('/{id}', [EstudanteController::class, 'update']);
    });

    Route::prefix('turma')->group(function () {
        Route::get('/', [TurmaController::class, 'index']);
        Route::get('/{id}', [TurmaController::class, 'show']);
        Route::post('/', [TurmaController::class, 'save']);
        Route::put('/{id}', [TurmaController::class, 'update']);
    });

    Route::prefix('curso')->group(function () {
        Route::get('/', [CursoController::class, 'index']);
        Route::get('/{id}', [CursoController::class, 'show']);
        Route::post('/', [CursoController::class, 'save']);
        Route::put('/{id}', [CursoController::class, 'update']);
    });
});
