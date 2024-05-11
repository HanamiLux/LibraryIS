package com.example.librarymobile

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.CheckBox
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.librarymobile.data.Book

class BookAdapter(private val context: Context, private var booksList: List<Book>) : RecyclerView.Adapter<BookAdapter.ViewHolder>() {
    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): BookAdapter.ViewHolder {
        val view = LayoutInflater.from(context).inflate(R.layout.book_card, parent, false)
        return ViewHolder(view)
    }

    override fun onBindViewHolder(holder: BookAdapter.ViewHolder, position: Int) {
        holder.apply{
            val book = booksList[position]
            titleTW.text = book.Title
            authorTW.text = "${book.Author.Firstname} ${book.Author.Lastname}"
            genreTW.text = book.Genre.Genrename
            isAvailableCB.isChecked = book.Isavailable
            quantityTW.text = "${book.Availablecopies}"
            publishedDateTW.text = "${book.Yearpublished}"
        }
    }

    override fun getItemCount(): Int {
        return booksList.size
    }

    fun setData(newBooks: List<Book>) {
        booksList = newBooks
        notifyDataSetChanged()
    }

    inner class ViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val titleTW: TextView = itemView.findViewById(R.id.title)
        val authorTW: TextView = itemView.findViewById(R.id.author)
        val genreTW: TextView = itemView.findViewById(R.id.genre)
        val publishedDateTW: TextView = itemView.findViewById(R.id.publishedDate)
        val quantityTW: TextView = itemView.findViewById(R.id.quantity)
        val isAvailableCB: CheckBox = itemView.findViewById(R.id.isAvailable)

    }


}