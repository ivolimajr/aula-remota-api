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
    {
        Schema::create('edrivings', function (Blueprint $table) {
            $table->id('idEdriving');
            $table->string('nome', 50);
            $table->string('email', 50);
            $table->string('telefone', 13);
            $table->string('cpf', 11);
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
