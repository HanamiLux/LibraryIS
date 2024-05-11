package com.example.librarymobile

import android.annotation.SuppressLint
import android.content.Context
import android.content.Intent
import android.opengl.Visibility
import android.os.Bundle
import android.text.Editable
import android.text.TextWatcher
import android.view.ContextMenu
import android.view.View
import android.widget.Toast
import androidx.activity.enableEdgeToEdge
import androidx.appcompat.app.AppCompatActivity
import androidx.core.content.res.ResourcesCompat
import androidx.core.view.ViewCompat
import androidx.core.view.WindowInsetsCompat
import androidx.core.widget.doOnTextChanged
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.librarymobile.data.Book
import com.example.librarymobile.data.GetAllResponse
import com.example.librarymobile.databinding.ActivityMainBinding
import com.example.librarymobile.interfaces.LibraryApiInterface
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.create
import java.util.Locale

class MainActivity : AppCompatActivity() {
    private lateinit var binding: ActivityMainBinding
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main)) { v, insets ->
            val systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars())
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom)
            insets
        }
        val books: ArrayList<Book> = arrayListOf()
        val getAllBooks: Call<GetAllResponse> = ServiceBuilder.buildRequest()
            .create(LibraryApiInterface::class.java).getAllBooks()
        getAllBooks.enqueue(object : Callback<GetAllResponse> {
            override fun onResponse(
                call: Call<GetAllResponse>,
                response: Response<GetAllResponse>
            ) {
                binding.apply {
                    textView.visibility = View.GONE
                    searchBar.visibility = View.VISIBLE
                    orderBtn.visibility = View.VISIBLE

                    if (response.isSuccessful && !response.body()?.ResponseData.isNullOrEmpty()) {
                        for (book in response.body()!!.ResponseData) {
                            if (book.Isavailable)
                                books.add(book)
                        }

                        if (books.isEmpty()) {
                            textView.visibility = View.VISIBLE
                            searchBar.visibility = View.GONE
                            orderBtn.visibility = View.GONE
                        }
                        recyclerView.layoutManager = LinearLayoutManager(this@MainActivity)
                        recyclerView.setHasFixedSize(true)
                        recyclerView.adapter = BookAdapter(this@MainActivity, books)
                    } else {
                        textView.visibility = View.VISIBLE
                        searchBar.visibility = View.GONE
                        orderBtn.visibility = View.GONE
                    }
                }
            }

            override fun onFailure(call: Call<GetAllResponse>, t: Throwable) {
                Toast.makeText(applicationContext, "$t", Toast.LENGTH_LONG).show()
            }

        })
        var isFilterUsed = false
        binding.apply {
            goBackBtn.setOnClickListener {
                val sharedPreferences = getSharedPreferences("token", Context.MODE_PRIVATE)
                sharedPreferences.edit().putString("token", "").apply()
                startActivity(Intent(applicationContext, SignInActivity::class.java))
                finish()
            }
            orderBtn.setOnClickListener {
                if (!isFilterUsed) {
                    isFilterUsed = true
                    orderBtn.setImageResource(R.drawable.ic_filter)
                    (recyclerView.adapter as? BookAdapter)?.setData(books.sortedBy { it.Yearpublished })
                } else {
                    isFilterUsed = false
                    orderBtn.setImageResource(R.drawable.ic_filter_used)
                    (recyclerView.adapter as? BookAdapter)?.setData(books.sortedByDescending { it.Yearpublished })
                }
            }
            binding.searchBar.addTextChangedListener(object : TextWatcher {
                override fun beforeTextChanged(p0: CharSequence?, p1: Int, p2: Int, p3: Int) {
                    // no need but it need
                }

                override fun onTextChanged(s: CharSequence?, start: Int, before: Int, count: Int) {
                    val searchText = s.toString().trim().lowercase(Locale.getDefault())
                    val filteredBooks = ArrayList<Book>()

                    for (book in books) {
                        if (book.Title.lowercase(Locale.getDefault()).contains(searchText)) {
                            filteredBooks.add(book)
                        }
                    }
                    (recyclerView.adapter as? BookAdapter)?.setData(filteredBooks)

                    binding.textView.visibility =
                        if (filteredBooks.isEmpty()) View.VISIBLE else View.GONE
                }

                override fun afterTextChanged(p0: Editable?) {
                    // no need but it need
                }
            })

        }
    }
}