<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateTableTurmas extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('turmas', function (Blueprint $table) {
            $table->id('id');
            $table->string('cursoId', 50);
            $table->string('instrutorId', 50);
            $table->string('turma', 50);
            $table->string('alunos', 50);
            $table->string('link', 50);
            $table->dateTime('dataInicio');
            $table->dateTime('dataFim');
            $table->string('matricula', 50);
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
        Schema::dropIfExists('turmas');
    }
}
