import java.security.MessageDigest
import java.security.SecureRandom
import java.util.Base64

object PasswordHelper {
    fun hashPassword(password: String): String {
        val sha256 = MessageDigest.getInstance("SHA-256")
        val saltBytes = ByteArray(32)
        val rng = SecureRandom()
        rng.nextBytes(saltBytes)

        val salt = Base64.getEncoder().encodeToString(saltBytes)
        val saltedPassword = (password + salt).toByteArray(Charsets.UTF_8)
        val hashBytes = sha256.digest(saltedPassword)
        val hash = Base64.getEncoder().encodeToString(hashBytes)
        return "$salt$hash"
    }

    fun verifyPassword(password: String, hashedPassword: String): Boolean {
        val parts = hashedPassword.split('=')
        if (parts.size != 3)
            return false

        val salt = parts[0] + "="
        val hash = parts[1] + "="

        val sha256 = MessageDigest.getInstance("SHA-256")
        val saltedPassword = (password + salt).toByteArray(Charsets.UTF_8)
        val hashBytes = sha256.digest(saltedPassword)
        val hashToCheck = Base64.getEncoder().encodeToString(hashBytes)
        return hashToCheck == hash
    }
}