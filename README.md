# BobyamaWalking
Unityで遺伝的アルゴリズムを使って歩き方を学習させるプログラムです。
個人的な備忘録としてまとめたもので、読めたもんじゃないと思いますがご容赦下さい。

一応簡単に各ファルの説明をしておきます。

Generation.cs
第一世代の初期化、世代交代の際の自然選択、最優秀個体をプレハブ化して保存するなどの役割を担っています。

Indivisual.cs
各個体の遺伝子リスト、関節のリストなどを持っています。

JointController.cs
遺伝子リストを元に、各関節に力を加えます。

JointCollector.cs
各関節に対応するJointController.csを集めたリストを作ります。

Matrix3x3.cs
逆行列を計算します。遺伝子から力を入れる角度を計算するのに使います。

MyComparer.cs
二つの値の組みを比較します。

CameraController.cs
カメラをキーボードで制御します。

CanvasController.cs
各世代のデータを画面上に書き出します。
