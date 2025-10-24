<?php
session_start();

// Session'da liste yoksa boş dizi oluştur
if (!isset($_SESSION['urunler'])) {
    $_SESSION['urunler'] = [];
}

// Ürün ekleme
if ($_SERVER['REQUEST_METHOD'] == 'POST' && !empty($_POST['urun'])) {
    $yeni_urun = trim($_POST['urun']);
    if (!empty($yeni_urun)) {
        $_SESSION['urunler'][] = $yeni_urun;
    }
}
?>

<!DOCTYPE html>
<html>
<head>
    <title>Alışveriş Listesi</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            min-height: 100vh;
            margin: 0;
            background-color: #f0f0f0;
        }
        
        .container {
            text-align: center;
            background: white;
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            width: 400px;
        }
        
        input[type="text"] {
            width: 70%;
            padding: 12px;
            border: 2px solid #ddd;
            border-radius: 8px;
            font-size: 16px;
            margin-right: 10px;
        }
        
        button {
            padding: 12px 20px;
            background: #4CAF50;
            color: white;
            border: none;
            border-radius: 8px;
            cursor: pointer;
            font-size: 16px;
        }
        
        .liste {
            margin-top: 30px;
            text-align: left;
        }
        
        .urun-item {
            padding: 10px;
            border-bottom: 1px solid #eee;
        }
    </style>
</head>
<body>
    <div class="container">
        <h1>Alışveriş Listesi</h1>
        
        <!-- Ürün Ekleme Formu -->
        <form method="POST">
            <input type="text" name="urun" placeholder="Ürün adı..." required>
            <button type="submit">Ekle</button>
        </form>

        <!-- Ürün Listesi -->
        <div class="liste">
            <h3>Listem (<?php echo count($_SESSION['urunler']); ?> ürün)</h3>
            
            <?php if (empty($_SESSION['urunler'])): ?>
                <p>Liste boş.</p>
            <?php else: ?>
                <?php foreach ($_SESSION['urunler'] as $index => $urun): ?>
                    <div class="urun-item">
                        <?php echo ($index + 1) . '. ' . htmlspecialchars($urun); ?>
                    </div>
                <?php endforeach; ?>
            <?php endif; ?>
        </div>
    </div>
</body>
</html>