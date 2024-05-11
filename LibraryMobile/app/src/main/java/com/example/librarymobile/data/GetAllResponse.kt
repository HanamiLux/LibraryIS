package com.example.librarymobile.data

data class GetAllResponse(
	val Message: String,
	val ResponseData: List<Book>,
	val Code: String,
	val id: String
)

