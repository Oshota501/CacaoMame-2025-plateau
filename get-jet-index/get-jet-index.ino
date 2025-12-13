#include <M5Unified.h>

// 画面のちらつきを防ぐためのスプライト（仮想画面）
M5Canvas canvas(&M5.Display);

float pitch, roll;

// キャラクタ設定
const int FACE_X = 160;
const int FACE_Y = 120;
const int FACE_W = 140; // 顔の幅
const int FACE_H = 120; // 顔の高さ

// パーツの移動設定
const int FACE_OFFSET_MAX = 15; // パーツの最大移動量（少し大きめに変更）
const int EYE_Y_OFFSET = -10;   // 顔の中心からの目の高さ
const int EYE_X_SPACING = 35;   // 目の間隔

// 色の定義（RGB565フォーマット）
const uint16_t COLOR_BG     = 0xEF5D; // 薄いピンク背景
const uint16_t COLOR_BODY   = 0xFFFF; // 白い体
const uint16_t COLOR_CHEEK  = 0xFCCC; // ピンクのほっぺ
const uint16_t COLOR_OUTLINE= 0x0000; // 黒い輪郭線

// まばたき用変数
unsigned long lastBlinkTime = 0;
bool isBlinking = false;
int blinkInterval = 3000; 

void setup() {
  auto cfg = M5.config();
  M5.begin(cfg);

  // スプライトの初期化
  canvas.createSprite(M5.Display.width(), M5.Display.height());
}

// 引数を「瞳の動き」から「パーツ全体の動き(offset)」に変更
void drawFace(int offsetX, int offsetY) {
  // 1. 背景を塗りつぶし
  canvas.fillScreen(COLOR_BG);

  // 2. 体（顔の輪郭）を描画
  // ★ここは offset を足さずに固定位置に描画します
  canvas.fillEllipse(FACE_X, FACE_Y, FACE_W + 4, FACE_H + 4, COLOR_OUTLINE);
  canvas.fillEllipse(FACE_X, FACE_Y, FACE_W, FACE_H, COLOR_BODY);

  // --- これ以降のパーツ座標に offsetX, offsetY を足します ---

  // 3. ほっぺた（チーク）
  canvas.fillEllipse(FACE_X - 50 + offsetX, FACE_Y + 20 + offsetY, 15, 8, COLOR_CHEEK);
  canvas.fillEllipse(FACE_X + 50 + offsetX, FACE_Y + 20 + offsetY, 15, 8, COLOR_CHEEK);

  // 4. 目を描画
  // 基準位置にオフセットを加算
  int leftEyeX = FACE_X - EYE_X_SPACING + offsetX;
  int rightEyeX = FACE_X + EYE_X_SPACING + offsetX;
  int currentEyeY = FACE_Y + EYE_Y_OFFSET + offsetY;

  if (isBlinking) {
    // まばたき中（線を描画）
    canvas.fillRect(leftEyeX - 15, currentEyeY - 2, 30, 4, COLOR_OUTLINE);
    canvas.fillRect(rightEyeX - 15, currentEyeY - 2, 30, 4, COLOR_OUTLINE);
  } else {
    // 開いている目
    
    // 黒目（全体）
    canvas.fillEllipse(leftEyeX, currentEyeY, 12, 18, COLOR_OUTLINE);
    canvas.fillEllipse(rightEyeX, currentEyeY, 12, 18, COLOR_OUTLINE);

    // ハイライト
    // ★目と一緒に動くため、leftEyeX などの基準座標からの相対位置は固定します
    canvas.fillCircle(leftEyeX - 4, currentEyeY - 6, 4, WHITE);
    canvas.fillCircle(rightEyeX - 4, currentEyeY - 6, 4, WHITE);
  }

  // 5. 口
  int mouthX = FACE_X + offsetX;
  int mouthY = FACE_Y + 35 + offsetY;

  // 上を向いているとき（offsetYがマイナス）は驚いた口にするなどの分岐
  if (offsetY < -5) {
      canvas.fillCircle(mouthX, mouthY, 6, COLOR_OUTLINE);
      canvas.fillCircle(mouthX, mouthY, 3, 0xFD20); 
  } else {
      // ニコニコ口
      canvas.fillEllipse(mouthX, mouthY, 8, 5, COLOR_OUTLINE);
      canvas.fillEllipse(mouthX, mouthY - 2, 8, 5, COLOR_BODY); // 上半分を消して半月型に
  }

  // 6. 画面に転送
  canvas.pushSprite(0, 0);
}

void loop() {
  M5.update();

  float ax, ay, az;
  M5.Imu.getAccel(&ax, &ay, &az);

  // 傾き計算
  roll  = atan2(ay, az) * 180 / PI;
  pitch = atan2(-ax, sqrt(ay * ay + az * az)) * 180 / PI;

  // 傾きを正規化
  float rollN  = constrain(roll,  -30, 30) / 30.0;
  float pitchN = constrain(pitch, -30, 30) / 30.0;

  // パーツ全体の移動量計算
  int featureDX = rollN  * FACE_OFFSET_MAX;
  int featureDY = pitchN * FACE_OFFSET_MAX;

  // まばたきの制御
  unsigned long currentTime = millis();
  if (isBlinking) {
    if (currentTime - lastBlinkTime > 150) {
      isBlinking = false;
      lastBlinkTime = currentTime;
      blinkInterval = random(1000, 4000);
    }
  } else {
    if (currentTime - lastBlinkTime > blinkInterval) {
      isBlinking = true;
      lastBlinkTime = currentTime;
    }
  }

  drawFace(featureDX, featureDY);

  delay(16);
}