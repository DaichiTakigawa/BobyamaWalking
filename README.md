# UnityWalking
Unityで遺伝的アルゴリズムを使って歩き方を学習させるプログラム。

Generation.cs
第一世代の初期化、世代交代の際の自然選択、最優秀個体をプレハブ化して保存する。

Indivisual.cs
各個体の遺伝子リスト、関節のリストなどを持つ。

JointController.cs
遺伝子リストを元に、各関節に力を加る。

JointCollector.cs
各関節に対応するJointController.csを集めたリストを作る。

Matrix3x3.cs
逆行列を計算します。遺伝子から力を入れる角度を計算するのに使う。

MyComparer.cs
二つの値の組みを比較。

CameraController.cs
カメラをキーボードで制御。

CanvasController.cs
各世代のデータを画面上に書き出す。
