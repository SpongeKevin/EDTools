## EDTools
[Encryption & Decryption Tools](https://sponge-edtools.netlify.com)

由於有使用各種加密算法的需求，而最近也在研究微軟的 Blazor、.NET Core 3.1，因此使用了這些技術實現了一個線上的加解密工具，使用的 Blazor 為 client side only 的版本，可發行為靜態頁面並且於 GitPage、FireBase 等地部屬，同時他僅使用 webassembly 在瀏覽器上執行，不會也不需要使用後端伺服器，讓你在加解密重要資訊時更加安心

將會持續完善加解密算法 & 測試，確保加解密如預期般運行
歡迎提 Issues & Pull requests

### 功能簡介

提供各種加密算法的線上加解密工具

### 目錄結構

```
EDTools
│
├─EDTools 網站方案
│  ├─Helper 加解密核心
│  ├─Pages 顯示頁面
│  ├─Shared 共用頁面布局
│  └─wwwroot 靜態資源，如 bootstrap 
│
└─EDToolsTest 測試方案
    └─AesTest.cs Aes 加解密測試      
```

### 單元測試

透過 EDToolsTest 這個單元測試專案進行，通過測試才會發佈

### 技術選擇

- [C#](https://docs.microsoft.com/zh-tw/dotnet/csharp/)
- [.NET Core 3.1](https://docs.microsoft.com/zh-tw/dotnet/core/)
- [Blazor Client Side](https://docs.microsoft.com/zh-tw/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1)

## 未來計畫
- [x] 單元測試
- [ ] DES
- [ ] RSA
- [ ] RC2

## License

The [MIT License](LICENSE)
