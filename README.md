# PCOT
## はじめに

PCOTは、[ぬるっぽ](https://twitter.com/pcotnullpo)様の開発している翻訳支援ツールです。

ぬるっぽ様の了承を得て、代理でソースGitHubに公開しています。
![pcot](https://user-images.githubusercontent.com/10492222/110198011-b14ef980-7e92-11eb-907e-5771a79f2b30.jpg)

## ビルドと起動
【必読】PCOTのソース説明.txt」を読んでください。

筆者はMicrosoft Visual Studio Community 2019で動作を確認しています。
### ビルド

#### ビルド失敗事例
> 2>------ すべてのリビルド開始: プロジェクト:PCOT, 構成: Debug x86 ------
2>C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Microsoft.Common.CurrentVersion.targets(2123,5): warning MSB3245: この参照を解決できませんでした。アセンブリ "Windows" が見つかりませんでした。アセンブリが間違いなくディスクに存在することを確認してください。 コードにこの参照が必要な場合、コンパイル エラーが発生する可能性があります。

と出たときは、PCにある「Windows.winmd」を参照すれば解決するかもしれません。

### 起動
ビルド成功後、PCOTを起動するためには、[翻訳支援ツールPCOT](http://www.gc-net.jp/s_54/)にある「PCOTのソース」をダウンロードして、中にあるファイルをコビーする必要があります。
