<?php

namespace App\Http\Controllers;

use App\Http\Controllers\Controller;
use Illuminate\Support\Facades\Auth;
use App\Providers\RouteServiceProvider;
use App\Models\User;
use Illuminate\Foundation\Auth\RegistersUsers;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Validator;

use Illuminate\Http\Request;

class AuthController extends Controller
{   
    public function dashboard()
    {
        if(Auth::check() === true){
            dd(Auth::user());
            return view('admin.dashboard');
        }
        
        return redirect()->route('admin.login');
    }

    public function showLoginForm()
    {
        return view('admin.formLogin');
    }

    public function login(Request $request)
    {
        var_dump($request->all());

        $credenciais =[
            'email' => $request->email,
            'password' => $request->password
        ];

        Auth::attempt($credenciais);
    }

    public function create(array $data)
    {
        return User::create([
            'name' => $data['name'],
            'email' => $data['email'],
            'password' => Hash::make($data['password']),
        ]);
    }
}
