<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableEdrivings extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {/* No print que o Yuri mandou, ta pedindo para ser feito mais dois campos: senha e confirmar senha. 
        Não vejo necessidade de fazer eles pois senha é dada para o usuario e confirmação de senha é uma tarefa feita no front*/
        Schema::create('edrivings', function (Blueprint $table) {
            $table->id('idEdriving');
            $table->string('fullName', 50);
            $table->string('email', 50);
            $table->string('cpf', 11);
            $table->string('telefone', 13);
            $table->boolean('status');
            $table->string('cargo', 50);
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
        Schema::dropIfExists('edrivings');
    }
}
