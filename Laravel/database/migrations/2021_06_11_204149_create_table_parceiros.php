<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableParceiros extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {/* O modelo de parceiros na versão passada tinha menos campos, todos os novos
        campos foram adicionados com exceção do confirmar senha e senha */
        Schema::create('parceiros', function (Blueprint $table) {
            $table->id('id');
            $table->string('fullName', 50);
            $table->string('email', 50);
            $table->boolean('status');
            $table->string('telefone', 13);
            $table->string('cep', 8);
            $table->string('cnpj', 14);
            $table->string('cargo', 50);
            $table->string('bairro', 100);
            $table->string('cidade', 100);
            $table->string('descricao', 150);
            $table->string('enderecoLogradouro', 100);
            $table->string('numero', 5);
            $table->string('uf', 3);
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
        Schema::dropIfExists('parceiros');
    }
}
