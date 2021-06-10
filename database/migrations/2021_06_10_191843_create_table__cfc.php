<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableCfc extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('cfcs', function (Blueprint $table) {
            $table->id('idCfc');
            $table->string('razaoSocial', 100);
            $table->string('nomeFantasia',100);
            $table->string('cnpj', 14);
            $table->string('inscricaoEstadual', 9);
            $table->dateTime('datadaFundacao');
            $table->string('enderecoLogradouro', 100);
            $table->string('numero', 5);
            $table->string('bairro', 100);
            $table->string('cidade', 100);
            $table->string('uf', 2);
            $table->string('cep', 8);
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
        Schema::dropIfExists('cfcs');
    }
}
