package com.example.librarymobile.data

data class Book(
    val Authorid: Int,
    val Title: String,
    val Genreid: Int,
    val Isavailable: Boolean,
    val Publisher: Publisher,
    val Isbn: String,
    val Yearpublished: Int,
    val Availablecopies: Int,
    val Publisherid: Int,
    val Author: Author,
    val Id: Int,
    val Genre: Genre,
)
