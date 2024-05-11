package com.example.librarymobile

import PasswordHelper.hashPassword
import PasswordHelper.verifyPassword
import android.content.Context
import android.content.Intent
import android.content.SharedPreferences
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.example.librarymobile.data.PasswordResponse
import com.example.librarymobile.data.User
import com.example.librarymobile.databinding.ActivitySignInBinding
import com.example.librarymobile.interfaces.LibraryApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class SignInActivity : AppCompatActivity() {

    private lateinit var binding: ActivitySignInBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivitySignInBinding.inflate(layoutInflater)
        setContentView(binding.root)
        val sharedPreferences = getSharedPreferences("token", Context.MODE_PRIVATE)
        if(sharedPreferences.getString("token", "") != ""){
            startActivity(Intent(applicationContext, MainActivity::class.java))
            finish()
        }
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        var hashedPassword: String = ""
        binding.apply {
            goToSignUpBtn.setOnClickListener{
                startActivity(Intent(applicationContext, SignUpActivity::class.java))
                finish()
            }
            signInBtn.setOnClickListener {
                val login = loginET.text.toString()
                val getPassword: Call<PasswordResponse> = ServiceBuilder.buildRequest().create(LibraryApiInterface::class.java).getPassword(login)
                getPassword.enqueue(object : Callback<PasswordResponse> {
                    override fun onResponse(call: Call<PasswordResponse>, response: Response<PasswordResponse>) {
                        if (response.isSuccessful) {
                            Toast.makeText(applicationContext, "Пароль обрабатывается", Toast.LENGTH_SHORT).show()
                             hashedPassword = response.body()?.responseData.toString()
                            if(!verifyPassword(passwordET.text.toString(), hashedPassword)) {
                                Toast.makeText(applicationContext, "Неправильный пароль", Toast.LENGTH_SHORT).show()
                                return
                            }
                            val newUser = User(login, hashedPassword)
                            val getToken: Call<PasswordResponse> = ServiceBuilder.buildRequest().create(LibraryApiInterface::class.java).authorize(newUser)
                            getToken.enqueue(object : Callback<PasswordResponse> {
                                override fun onResponse(call: Call<PasswordResponse>, response: Response<PasswordResponse>) {
                                    if (response.isSuccessful) {
                                        sharedPreferences.edit()
                                            .putString("token", response.body()
                                                ?.responseData.toString()).apply()
                                        startActivity(Intent(applicationContext, MainActivity::class.java))
                                        finish()
                                    } else {
                                        Toast.makeText(applicationContext, "Некорректные данные", Toast.LENGTH_SHORT).show()
                                    }
                                }

                                override fun onFailure(call: Call<PasswordResponse>, t: Throwable) {
                                    Toast.makeText(applicationContext, "Failed to sign in" + t.message, Toast.LENGTH_SHORT).show()
                                }
                            })
                        } else {
                            Toast.makeText(applicationContext, "Такого пользователя не существует", Toast.LENGTH_SHORT).show()
                        }
                    }
                    override fun onFailure(call: Call<PasswordResponse?>, t: Throwable) {
                        Toast.makeText(applicationContext, "Failed to get data" + t.message, Toast.LENGTH_SHORT).show()
                    }
                })

            }
        }
    }
}