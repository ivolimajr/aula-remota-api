<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableCfcs extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {/* No print que o Yuri mandou, ta pedindo para ser feito mais dois campos: senha e confirmar senha. 
        Não vejo necessidade de fazer eles pois senha é dada para o usuario e confirmação de senha é uma tarefa feita no front*/
        Schema::create('cfcs', function (Blueprint $table) {
            $table->id('idCfc');
            $table->string('fullName', 50);
            $table->string('email', 50);
            $table->string('telefone', 13);
            $table->boolean('status');
            $table->string('bairro', 100);
            $table->string('cep', 8);
            $table->string('cidade', 100);
            $table->string('cnpj', 14);
            $table->dateTime('datadaFundacao'); 
            $table->string('enderecoLogradouro', 100);
            $table->string('inscricaoEstadual', 9);
            $table->string('localizacaoLatitude', 13);
            $table->string('longitude', 13);
            $table->string('nomeFantasia',100);
            $table->string('numero', 5);
            $table->string('razaoSocial', 100);   
            $table->string('site', 50);
            $table->string('uf', 2);
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
