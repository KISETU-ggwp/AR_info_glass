# Drag_And_Drop2.cs

XREAL社が公開している”NR SDK”に対応した字幕の位置制御スクリプトになります。

このプログラムではText(TMP)をAR上で制御されているレーザーポインタを当て、特定のボタン(UnityのButton-UI)をワンタップします。そうすると、レーザーポインタに向けて
ゆっくりと移動することができるものです。

注意点として、Ｚ座標は有効にしていないためＸとＹの座標方向のみ移動が可能になっています。

なお、アタッチ先は[NRVirtualDisplayer/VirtualController]にAdd Componentしてください。確か、それでいいはずです。一つ気を付けることとして
Object Holderの登録が重要です。

[NRInput/Right/controllerTracker/LaserRaycastor/Reticle]の中にObjectHolderという名前のGameObjectを新規作成してはめてください。新規作成するとデフォルトで
TransformがあるのでそれをAdd Componentしたところの”Object Holder”にはめてください。
あとは、Unity内でButton-UIの登録をしてください。

以下は一例です。
1. [NRVirtualDisplayer/VirtualController/Canvas/BaseControllPanel/Buttons]にbuttonを新規作成。
2. Inspector内で、OnClickの設定をする。
  1. Runtime　Add Componentしたオブジェクトをはめる Drag_And_Drop2を選択して、HandleButtonを選択
3. Done





