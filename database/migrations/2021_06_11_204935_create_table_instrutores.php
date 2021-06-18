<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableInstrutores extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('instrutores', function (Blueprint $table) {
            $table->id('idInstrutores');
            $table->string('fullName', 50);
            $table->string('email', 50);
            $table->string('cpf',11);
            $table->string('identidade',9);
            $table->string('telefone', 13);
            $table->boolean('status');
            $table->string('cargo',50);
            $table->string('cep',8);
            $table->string('bairro',50);
            $table->string('cidade',50);
            $table->string('uf',2);
            $table->string('numero',10);
            $table->dateTime('dataNascimento');
            $table->string('enderecoLogradouro', 150);
            $table->string('orgaoexpedidor');
            $table->string('site', 50);
            $table->string('cursos', 100);
            $table->string('uploadDoc', 100);
            
            

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
        Schema::dropIfExists('instrutors');
    }
}
