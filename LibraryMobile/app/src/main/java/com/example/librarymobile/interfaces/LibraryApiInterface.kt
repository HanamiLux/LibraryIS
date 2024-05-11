package com.example.librarymobile.interfaces

import com.example.librarymobile.data.GetAllResponse
import com.example.librarymobile.data.PasswordResponse
import com.example.librarymobile.data.User
import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.GET
import retrofit2.http.POST

interface LibraryApiInterface {
    @GET("LibraryApi/Books/getall/")
    fun getAllBooks(): Call<GetAllResponse>
    @POST("LibraryApi/Users/Create/")
    fun addUser(@Body newUser: User): Call<User>
    @POST("Login/")
    fun authorize(@Body newUser: User): Call<PasswordResponse>
    @POST("Login/GetPassword/")
    fun getPassword(@Body name: String): Call<PasswordResponse>
}



