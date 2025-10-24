import tkinter as tk
from tkinter import ttk, messagebox
import requests

def get_exchange_rates():
    try:
        url = "https://open.er-api.com/v6/latest/USD"
        response = requests.get(url, timeout=5)
        data = response.json()
        rates = data["rates"]

        selected = ["USD", "EUR", "GBP", "JPY", "CHF"]
        # USD bazlı veriden TL karşılığını çıkarıyoruz
        return {cur: rates["TRY"] / rates[cur] for cur in selected}
    except Exception as e:
        messagebox.showerror("Hata", f"Kurlar alınamadı!\n{e}")
        return {}

root = tk.Tk()
root.title("💱 Döviz Kuru Takip ve Hesaplama")
root.geometry("550x460")
root.configure(bg="#f4f6f9")

style = ttk.Style()
style.theme_use("clam")
style.configure("Treeview", background="#ffffff", foreground="#000000", rowheight=30, fieldbackground="#ffffff")
style.map('Treeview', background=[('selected', '#dbeafe')])
style.configure("TButton", font=("Arial", 11, "bold"), padding=8)
style.configure("TLabel", background="#f4f6f9", font=("Arial", 11))
style.configure("TEntry", padding=5)

title = ttk.Label(root, text="💹 Güncel Döviz Kurları (Canlı Veriler)", font=("Arial", 15, "bold"))
title.pack(pady=10)

tree = ttk.Treeview(root, columns=("Tur", "Kur"), show="headings", height=6)
tree.heading("Tur", text="Döviz Türü")
tree.heading("Kur", text="1 Birim (TL)")
tree.column("Tur", anchor="center", width=150)
tree.column("Kur", anchor="center", width=200)
tree.pack(pady=10)

def update_table():
    tree.delete(*tree.get_children())
    rates = get_exchange_rates()
    if not rates:
        return
    for currency, rate in rates.items():
        tree.insert("", "end", values=(currency, f"{rate:.2f} TL"))
    currency_combo["values"] = list(rates.keys())
    currency_combo.current(0)
    global EXCHANGE_RATES
    EXCHANGE_RATES = rates

frame = ttk.Frame(root)
frame.pack(pady=20)

amount_label = ttk.Label(frame, text="Miktar:")
amount_label.grid(row=0, column=0, padx=5, pady=5)
amount_entry = ttk.Entry(frame, width=10)
amount_entry.grid(row=0, column=1, padx=5, pady=5)

currency_label = ttk.Label(frame, text="Kur Türü:")
currency_label.grid(row=0, column=2, padx=5, pady=5)
currency_var = tk.StringVar()
currency_combo = ttk.Combobox(frame, textvariable=currency_var, state="readonly", width=6)
currency_combo.grid(row=0, column=3, padx=5, pady=5)

result_label = ttk.Label(root, text="", font=("Arial", 13, "bold"), foreground="#2563eb")
result_label.pack(pady=10)

def hesapla():
    try:
        miktar = float(amount_entry.get())
        secilen_kur = currency_var.get()
        tl_deger = miktar * EXCHANGE_RATES[secilen_kur]
        result_label.config(text=f"{miktar:.2f} {secilen_kur} = {tl_deger:.2f} TL")
    except ValueError:
        messagebox.showerror("Hata", "Lütfen geçerli bir miktar giriniz!")
    except Exception as e:
        messagebox.showerror("Hata", f"Bir hata oluştu:\n{e}")

calc_button = ttk.Button(root, text="Hesapla", command=hesapla)
calc_button.pack(pady=5)

refresh_button = ttk.Button(root, text="🔄 Kurları Güncelle", command=update_table)
refresh_button.pack(pady=5)

EXCHANGE_RATES = {}
update_table()

root.mainloop()
