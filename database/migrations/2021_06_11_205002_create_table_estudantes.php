<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableEstudantes extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('estudantes', function (Blueprint $table) {
            $table->id('idEstudantes');
            $table->string('fullName', 50);
            $table->string('email', 50);
            $table->string('cpf',11);
            $table->string('telefone', 13);
            $table->boolean('status');
            $table->string('cep',8);
            $table->string('cidade',50);
            $table->string('bairro',50);
            $table->string('uf',2);
            $table->string('enderecoLogradouro', 150);
            $table->string('numero',10);
            $table->string('curso', 100);
            $table->dateTime('dataNascimento');
            $table->string('identidade',9);
            $table->string('orgaoexpedidor');
            $table->string('turno', 10);
            $table->string('turma', 10);
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
        Schema::dropIfExists('estudantes');
    }
}
