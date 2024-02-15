なお、これらはNRSDKを導入していることを前提にお話しをしております。
はじめて触る方は先に以下の記事を参考にして進めてください。

[はじめて触る人が参考にすべき記事](https://zenn.dev/meson/articles/get-started-with-nreal-air)

## Drag_And_Drop2.cs

XREAL社が公開している”NR SDK”に対応した字幕の位置制御スクリプトになります。

このプログラムではText(TMP)をAR上で制御されているレーザーポインタを当て、特定のボタン(UnityのButton-UI)をワンタップします。そうすると、レーザーポインタに向けて
ゆっくりと移動することができるものです。

注意点として、Ｚ座標は有効にしていないためＸとＹの座標方向のみ移動が可能になっています。

なお、アタッチ先は[NRVirtualDisplayer/VirtualController]にAdd Componentしてください。確か、それでいいはずです。一つ気を付けることとしてObject Holderの登録が重要です。

[NRInput/Right/controllerTracker/LaserRaycastor/Reticle]の中にObjectHolderという名前のGameObjectを新規作成してはめてください。新規作成するとデフォルトで
TransformがあるのでそれをAdd Componentしたところの”Object Holder”にはめてください。
あとは、Unity内でButton-UIの登録をしてください。

以下は一例です。
1. [NRVirtualDisplayer/VirtualController/Canvas/BaseControllPanel/Buttons]にbuttonを新規作成。
2. Inspector内で、OnClickの設定をする。
  1. Runtimeを選択
  2. Add Componentしたオブジェクトをはめる
  3. Drag_And_Drop2を選択して、HandleButtonを選択
4. Done


## Srt2.cs

UnityのText(TMP)に対応した字幕出力を達成するスクリプトになります。srt形式ファイルに対応しており、タイムスタンプを基に字幕を更新するようになっています。
アタッチ先は、字幕を提示したいText(TMP)の中にしてください。これについては、GameObjectをText(TMP)内に新規作成してそこにアタッチしておくといいでしょう。
なお、複数の字幕ファイルに対応しているため一度に複数の字幕ファイルのいずれかを選択して字幕の出力を達成することができるので結構おすすめです。
今回の字幕ファイルの保存ディレクトリは[Assets/StreamingAssets]に入れてあることを想定してコードを作成していますのでその点ご注意ください。
コード作成者が、修正するとすれば、srt形式ファイルの名前をコード内で変更するだけだと思われます。今回であれば3つのsrt形式ファイルを想定していますのでButtonは３つ必要になりますのでそのあたりは調整してください。増やしたければ
[Path.Combine(Application.streamingAssetsPath, "xxx.srt"),]を追加しておけばいいでしょう。

## 最後に
なにか、このコードが役立つことを祈っております。なお、これらのコードはXREALコミュニティのアドバイスを基に自分でコードを作成したものになります。
なにか疑問点があれば *issue* にご投稿ください。

*それでは、よいUnity開発ライフを！！*





