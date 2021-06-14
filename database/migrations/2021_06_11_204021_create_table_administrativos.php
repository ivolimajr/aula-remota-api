<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableAdministrativos extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('administrativos', function (Blueprint $table) {
            $table->id('idAdministrativo');
            $table->string('nome', 50);
            $table->string('cargo',50);
            $table->string('cpf',11);
            $table->string('identidade',9);
            $table->string('orgaoexpedidor');
            $table->dateTime('dataNascimento');
            $table->string('enderecoLogradouro', 150);
            $table->string('numero',10);
            $table->string('bairro',50);
            $table->string('cidade',50);
            $table->string('uf',2);
            $table->string('cep',8);
            $table->string('localizacaoLatitude', 13);
            $table->string('longitude', 13);
            $table->string('telefone1', 13);
            $table->string('telefone2', 13);
            $table->string('email', 50);
            $table->string('site', 50);

            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('administrativos');
    }
}
