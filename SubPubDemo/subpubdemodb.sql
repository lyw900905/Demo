/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50610
Source Host           : localhost:3306
Source Database       : subpubdemodb

Target Server Type    : MYSQL
Target Server Version : 50610
File Encoding         : 65001

Date: 2016-09-20 22:01:20
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for userinfo
-- ----------------------------
DROP TABLE IF EXISTS `userinfo`;
CREATE TABLE `userinfo` (
  `UserName` varchar(255) DEFAULT NULL,
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserIntegral` int(255) DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userinfo
-- ----------------------------
INSERT INTO `userinfo` VALUES ('测试用户1', '1', '50');
INSERT INTO `userinfo` VALUES ('测试用户2', '2', '68');
INSERT INTO `userinfo` VALUES ('测试用户4', '3', '72');
INSERT INTO `userinfo` VALUES ('测试用户5', '4', '43');
INSERT INTO `userinfo` VALUES ('测试用户6', '5', '87');
INSERT INTO `userinfo` VALUES ('测试用户7', '6', '23');
INSERT INTO `userinfo` VALUES ('测试用户8', '7', '105');
INSERT INTO `userinfo` VALUES ('测试用户名', '8', '50');
INSERT INTO `userinfo` VALUES ('测试用户名', '9', '50');
