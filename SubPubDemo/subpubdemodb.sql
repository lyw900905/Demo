/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50610
Source Host           : localhost:3306
Source Database       : subpubdemodb

Target Server Type    : MYSQL
Target Server Version : 50610
File Encoding         : 65001

Date: 2016-09-22 23:34:51
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
) ENGINE=InnoDB AUTO_INCREMENT=49 DEFAULT CHARSET=utf8;

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
INSERT INTO `userinfo` VALUES ('测试用户名', '10', '50');
INSERT INTO `userinfo` VALUES ('测试用户名35', '11', '35');
INSERT INTO `userinfo` VALUES ('测试用户名5', '12', '5');
INSERT INTO `userinfo` VALUES ('测试用户名52', '13', '52');
INSERT INTO `userinfo` VALUES ('测试用户名42', '14', '42');
INSERT INTO `userinfo` VALUES ('测试用户名94', '15', '94');
INSERT INTO `userinfo` VALUES ('测试用户名57', '16', '57');
INSERT INTO `userinfo` VALUES ('测试用户名74', '17', '74');
INSERT INTO `userinfo` VALUES ('测试用户名48', '18', '48');
INSERT INTO `userinfo` VALUES ('测试用户名86', '19', '86');
INSERT INTO `userinfo` VALUES ('测试用户名48', '20', '48');
INSERT INTO `userinfo` VALUES ('测试用户名41', '21', '41');
INSERT INTO `userinfo` VALUES ('测试用户名54', '22', '54');
INSERT INTO `userinfo` VALUES ('测试用户名57', '23', '57');
INSERT INTO `userinfo` VALUES ('测试用户名54', '24', '54');
INSERT INTO `userinfo` VALUES ('测试用户名56', '25', '56');
INSERT INTO `userinfo` VALUES ('测试用户名40', '26', '40');
INSERT INTO `userinfo` VALUES ('测试用户名46', '27', '46');
INSERT INTO `userinfo` VALUES ('测试用户名33', '28', '33');
INSERT INTO `userinfo` VALUES ('测试用户名60', '29', '60');
INSERT INTO `userinfo` VALUES ('测试用户名57', '30', '57');
INSERT INTO `userinfo` VALUES ('测试用户名50', '31', '50');
INSERT INTO `userinfo` VALUES ('测试用户名38', '32', '38');
INSERT INTO `userinfo` VALUES ('测试用户名76', '33', '76');
INSERT INTO `userinfo` VALUES ('测试用户名41', '34', '41');
INSERT INTO `userinfo` VALUES ('测试用户名37', '35', '37');
INSERT INTO `userinfo` VALUES ('测试用户名28', '36', '28');
INSERT INTO `userinfo` VALUES ('测试用户名99', '37', '99');
INSERT INTO `userinfo` VALUES ('测试用户名94', '38', '94');
INSERT INTO `userinfo` VALUES ('测试用户名17', '39', '17');
INSERT INTO `userinfo` VALUES ('测试用户名9', '40', '9');
INSERT INTO `userinfo` VALUES ('测试用户名78', '41', '78');
INSERT INTO `userinfo` VALUES ('测试用户名88', '42', '88');
INSERT INTO `userinfo` VALUES ('测试用户名81', '43', '81');
INSERT INTO `userinfo` VALUES ('测试用户名49', '44', '49');
INSERT INTO `userinfo` VALUES ('测试用户名20', '45', '20');
INSERT INTO `userinfo` VALUES ('测试用户名72f0', '46', '97');
INSERT INTO `userinfo` VALUES ('测试用户名c75d', '47', '97');
INSERT INTO `userinfo` VALUES ('测试用户名f0fd', '48', '31');
