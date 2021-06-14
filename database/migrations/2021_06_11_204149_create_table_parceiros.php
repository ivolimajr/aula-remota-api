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
    {
        Schema::create('parceiros', function (Blueprint $table) {
            $table->id('idParceiro');
            $table->string('uf', 3);
            $table->string('nome', 50);
            $table->string('descricao', 150);
            $table->string('cnpj', 14);
            $table->string('email', 50);
            
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
