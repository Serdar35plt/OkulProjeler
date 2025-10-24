import tkinter as tk

sozluk = {
    "apple": "elma",
    "book": "kitap",
    "car": "araba",
    "dog": "köpek",
    "sun": "güneş"
}

pencere_renk = "#2c3e50"
buton_renk = "#3498db"
buton_yazi_renk = "#ecf0f1"
uyari_renk = "#e74c3c"
baslik_font = ("Comic Sans MS", 24, "bold")
buton_font = ("Arial", 14, "bold")
etiket_font = ("Arial", 12)

# Ana pencere
pencere = tk.Tk()
pencere.title("📘 İngilizce - Türkçe Sözlük")
pencere.geometry("400x350")
pencere.config(bg=pencere_renk)
pencere.resizable(True, True)

# Başlık
baslik = tk.Label(pencere, text="📘 İngilizce - Türkçe Sözlük", font=baslik_font, bg=pencere_renk, fg="#ecf0f1")
baslik.pack(pady=20)

# Fonksiyonlar için ortak pencere açıcı fonksiyonu
def pencere_ac(baslik_text, onay_func):
    p = tk.Toplevel(pencere)
    p.title(baslik_text)
    p.geometry("350x150")
    p.config(bg=pencere_renk)
    p.resizable(False, False)

    etiket = tk.Label(p, text=baslik_text, font=etiket_font, bg=pencere_renk, fg="#ecf0f1")
    etiket.pack(pady=10)

    giris = tk.Entry(p, font=etiket_font, width=30)
    giris.pack(pady=5)
    giris.focus()

    mesaj = tk.Label(p, text="", font=etiket_font, bg=pencere_renk, fg=uyari_renk)
    mesaj.pack(pady=5)

    def onay():
        kelime = giris.get().strip()
        if kelime == "":
            mesaj.config(text="Lütfen bir kelime girin!")
            return
        onay_func(kelime, mesaj, p)

    btn_onay = tk.Button(p, text="Onayla", bg=buton_renk, fg=buton_yazi_renk, font=buton_font, command=onay)
    btn_onay.pack(pady=10)

def kelime_ara():
    def ara(kelime, mesaj, pencere_kapama):
        anlam = sozluk.get(kelime.lower())
        if anlam:
            mesaj.config(text=f"✅ {kelime} → {anlam}", fg="lightgreen")
        else:
            mesaj.config(text="❌ Bu kelime sözlükte yok.", fg=uyari_renk)
        # 2 saniye sonra pencereyi kapat
        pencere_kapama.after(2000, pencere_kapama.destroy)

    pencere_ac("Kelime Ara", ara)

def kelime_ekle():
    p = tk.Toplevel(pencere)
    p.title("Kelime Ekle")
    p.geometry("350x220")
    p.config(bg=pencere_renk)
    p.resizable(False, False)

    etiket1 = tk.Label(p, text="İngilizce Kelime:", font=etiket_font, bg=pencere_renk, fg="#ecf0f1")
    etiket1.pack(pady=(15,5))
    ing_giris = tk.Entry(p, font=etiket_font, width=30)
    ing_giris.pack(pady=5)
    ing_giris.focus()

    etiket2 = tk.Label(p, text="Türkçe Karşılığı:", font=etiket_font, bg=pencere_renk, fg="#ecf0f1")
    etiket2.pack(pady=(15,5))
    tr_giris = tk.Entry(p, font=etiket_font, width=30)
    tr_giris.pack(pady=5)

    mesaj = tk.Label(p, text="", font=etiket_font, bg=pencere_renk, fg=uyari_renk)
    mesaj.pack(pady=10)

    def onay():
        ing = ing_giris.get().strip().lower()
        tr = tr_giris.get().strip().lower()
        if ing == "" or tr == "":
            mesaj.config(text="Lütfen her iki kelimeyi de girin!")
            return
        if ing in sozluk:
            mesaj.config(text="⚠️ Bu kelime zaten sözlükte var!")
            return
        sozluk[ing] = tr
        mesaj.config(text=f"✅ {ing} → {tr} eklendi!", fg="lightgreen")
        # 2 saniye sonra pencereyi kapat
        p.after(2000, p.destroy)

    btn_onay = tk.Button(p, text="Ekle", bg=buton_renk, fg=buton_yazi_renk, font=buton_font, command=onay)
    btn_onay.pack(pady=10)

def kelime_sil():
    def sil(kelime, mesaj, pencere_kapama):
        kelime = kelime.lower()
        if kelime in sozluk:
            del sozluk[kelime]
            mesaj.config(text=f"✅ {kelime} silindi.", fg="lightgreen")
        else:
            mesaj.config(text="❌ Bu kelime sözlükte yok.", fg=uyari_renk)
        pencere_kapama.after(2000, pencere_kapama.destroy)

    pencere_ac("Kelime Sil", sil)

def sozlugu_goster():
    if not sozluk:
        mesaj = tk.Label(pencere, text="Sözlük boş!", font=etiket_font, bg=pencere_renk, fg=uyari_renk)
        mesaj.pack(pady=10)
        pencere.after(3000, mesaj.destroy)
        return
    kelimeler = "\n".join([f"{ing} → {tr}" for ing, tr in sozluk.items()])
    goster_pencere = tk.Toplevel(pencere)
    goster_pencere.title("Sözlüğü Göster")
    goster_pencere.geometry("350x300")
    goster_pencere.config(bg=pencere_renk)

    etiket = tk.Label(goster_pencere, text="Sözlükteki Kelimeler", font=("Arial", 16, "bold"), bg=pencere_renk, fg="#ecf0f1")
    etiket.pack(pady=10)

    metin = tk.Text(goster_pencere, font=("Arial", 14), bg="#34495e", fg="#ecf0f1", wrap="word")
    metin.pack(padx=10, pady=10, fill="both", expand=True)
    metin.insert("1.0", kelimeler)
    metin.config(state="disabled")

# Butonlar
btn_ara = tk.Button(pencere, text="Kelime Ara", width=20, bg=buton_renk, fg=buton_yazi_renk, font=buton_font, relief="raised", bd=5, command=kelime_ara)
btn_ara.pack(pady=8)

btn_ekle = tk.Button(pencere, text="Kelime Ekle", width=20, bg=buton_renk, fg=buton_yazi_renk, font=buton_font, relief="raised", bd=5, command=kelime_ekle)
btn_ekle.pack(pady=8)

btn_sil = tk.Button(pencere, text="Kelime Sil", width=20, bg=buton_renk, fg=buton_yazi_renk, font=buton_font, relief="raised", bd=5, command=kelime_sil)
btn_sil.pack(pady=8)

btn_goster = tk.Button(pencere, text="Sözlüğü Göster", width=20, bg=buton_renk, fg=buton_yazi_renk, font=buton_font, relief="raised", bd=5, command=sozlugu_goster)
btn_goster.pack(pady=8)

btn_cikis = tk.Button(pencere, text="Çıkış", width=20, bg="#e74c3c", fg=buton_yazi_renk, font=buton_font, relief="raised", bd=5, command=pencere.destroy)
btn_cikis.pack(pady=15)

pencere.mainloop()
