package com.example.librarymobile

import PasswordHelper.hashPassword
import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import com.example.librarymobile.data.User
import com.example.librarymobile.databinding.ActivitySignUpBinding
import com.example.librarymobile.interfaces.LibraryApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class SignUpActivity : AppCompatActivity() {
    private lateinit var binding: ActivitySignUpBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivitySignUpBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        binding.apply {
            goBackBtn.setOnClickListener{
                startActivity(Intent(applicationContext, SignInActivity::class.java))
                finish()
            }

            confirmBtn.setOnClickListener {
                if(passwordET.text.toString() != passwordETRepeat.text.toString()) {
                    Toast.makeText(applicationContext, "Пароли не равны!", Toast.LENGTH_LONG).show()
                    return@setOnClickListener
                }
                val newUser = User(
                    loginET.text.toString(),
                    hashPassword(passwordET.getText().toString())
                )
                val addUserCall: Call<User> =
                    ServiceBuilder.buildRequest().create(LibraryApiInterface::class.java)
                        .addUser(newUser)
                addUserCall.enqueue(object : Callback<User> {
                    override fun onResponse(call: Call<User>, response: Response<User>) {
                        if (response.isSuccessful) {
                            Toast.makeText(
                                applicationContext,
                                "Пользователь добавлен успешно",
                                Toast.LENGTH_LONG
                            ).show()
                            startActivity(Intent(applicationContext, SignInActivity::class.java))
                            finish()
                        } else {
                            Toast.makeText(
                                applicationContext,
                                "Не удалось добавить пользователя",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                    }

                    override fun onFailure(call: Call<User>, t: Throwable) {
                        Toast.makeText(
                            applicationContext,
                            "Failed to add user" + t.message,
                            Toast.LENGTH_SHORT
                        ).show()
                    }
                })
            }
        }
    }

}

