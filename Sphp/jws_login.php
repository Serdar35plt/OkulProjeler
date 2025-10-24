<?php
session_start();

// Basit JWT fonksiyonu
function createJWT($data) {
    $secret = "okul_projesi";
    $header = json_encode(['typ' => 'JWT', 'alg' => 'HS256']);
    $payload = json_encode($data);
    
    $base64Header = str_replace(['+', '/', '='], ['-', '_', ''], base64_encode($header));
    $base64Payload = str_replace(['+', '/', '='], ['-', '_', ''], base64_encode($payload));
    
    $signature = hash_hmac('sha256', $base64Header . "." . $base64Payload, $secret, true);
    $base64Signature = str_replace(['+', '/', '='], ['-', '_', ''], base64_encode($signature));
    
    return $base64Header . "." . $base64Payload . "." . $base64Signature;
}

// Kullanıcılar
$users = [
    'serdar' => '123456',
    'admin' => 'admin123'
];

// Giriş kontrolü
$error = '';
if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $username = $_POST['username'] ?? '';
    $password = $_POST['password'] ?? '';
    
    if (isset($users[$username]) && $users[$username] === $password) {
        // Giriş başarılı - JWT oluştur
        $tokenData = [
            'username' => $username,
            'login_time' => time()
        ];
        $_SESSION['token'] = createJWT($tokenData);
        $loginSuccess = true;
    } else {
        $error = 'Yanlış kullanıcı adı veya şifre!';
        $loginSuccess = false;
    }
}
?>

<!DOCTYPE html>
<html>
<head>
    <title>Giriş Sistemi</title>
    <style>
        body {
            font-family: Arial;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
            background: #f0f0f0;
        }
        .container {
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            width: 350px;
            text-align: center;
        }
        input {
            width: 100%;
            padding: 12px;
            margin: 10px 0;
            border: 2px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            box-sizing: border-box;
        }
        button {
            width: 100%;
            padding: 12px;
            background: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 16px;
        }
        .error {
            color: red;
            margin-top: 15px;
        }
        .success {
            color: green;
            font-size: 24px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <div class="container">
        <?php if (isset($loginSuccess) && $loginSuccess): ?>
            <!-- Giriş başarılı -->
            <div class="success">✅ Giriş Başarılı!</div>
            <p>Hoş geldiniz <strong><?php echo htmlspecialchars($username); ?></strong></p>
            <p>JWT token oluşturuldu.</p>
            
        <?php else: ?>
            <!-- Giriş formu -->
            <h2>Giriş Yap</h2>
            <form method="POST">
                <input type="text" name="username" placeholder="Kullanıcı Adı" required>
                <input type="password" name="password" placeholder="Şifre" required>
                <button type="submit">Giriş Yap</button>
            </form>
            
            <?php if ($error): ?>
                <div class="error"><?php echo $error; ?></div>
            <?php endif; ?>
            
            <div style="margin-top: 20px; font-size: 14px; color: #666;">
                <strong>Test:</strong><br>
                serdar / 123456
            </div>
        <?php endif; ?>
    </div>
</body>
</html>