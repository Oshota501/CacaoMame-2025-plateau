/*
*******************************************************************************
* Copyright (c) 2021 by M5Stack
*                  Equipped with M5Core2 sample source code
*                          配套  M5Core2 示例源代码
* Visit for more information: https://docs.m5stack.com/en/core/core2
* 获取更多资料请访问: https://docs.m5stack.com/zh_CN/core/core2
*
* Describe: Button example.  按键示例
* Date: 2021/7/21
*******************************************************************************
*/
#include <M5Core2.h>

float pitch, roll;

void setup() {
  M5.begin();
  M5.IMU.Init();
}

void loop() {
  float ax, ay, az;
  M5.IMU.getAccelData(&ax, &ay, &az);

  // 傾き計算
  roll  = atan2(ay, az) * 180 / PI;
  pitch = atan2(-ax, sqrt(ay*ay + az*az)) * 180 / PI;

  // 表示（確認用）
  M5.Lcd.setCursor(0, 0);
  M5.Lcd.printf("Roll: %.2f\nPitch: %.2f", roll, pitch);

  delay(20);
}

