import AsyncStorage from '@react-native-async-storage/async-storage';


import React,{Component} from 'react';
import {StyleSheet, ImageBackground, View, Image, TextInput, TouchableOpacity, Text} from 'react-native'
import api from '../services/api';

export default class Login extends Component {
    constructor(props) {
        super(props);
        this.state = {
            email: 'mariana@email.com',
            senha: '777',
        };
    }

    realizarLogin = async () => {
        //nao temos mais  console log.
        //vamos utilizar console.warn.
    
        //apenas para teste.
        console.warn(this.state.email + ' ' + this.state.senha);
    
        const resposta = await api.post('/login', {
          email: this.state.email, //ADM@ADM.COM
          senha: this.state.senha, //senha123
        });

   

    if (resposta.status == 200){
        const token = resposta.data.token;
        await AsyncStorage.setItem('userToken', token);

        
        console.warn('Login Realizado');
        this.props.navigation.navigate('Listar');
    
    }

    // console.warn(token);

};
   
    render() {
        return (
            <ImageBackground

                source={require('../../assets/back-login.png')}
                style={StyleSheet.absoluteFillObject}>

                <View style={styles.main}>

                    <Image
                        source={require('../../assets/SpMedGroup-Logo.png')}
                        style={styles.mainImgLogin}
                    />

                    <TextInput
                        style={styles.inputLogin}
                       
                        // style={{ fontFamily: 'Poppins' ? 'OpenSans-Regular' : 'OpenSans-Italic' }}
                        placeholder="Email..."
                        placeholderTextColor="#FFF"
                        placeholderTextTransform ='capitalize'
                        keyboardType="email-address"
                        // style={{textTransform: 'capitalize'}}
                        // ENVENTO PARA FAZERMOS ALGO ENQUANTO O TEXTO MUDA
                        onChangeText={email => this.setState({ email })}
                    />

                    <TextInput
                        style={styles.inputLogin}
                        placeholder="Senha..."
                        placeholderTextColor="#FFF"
                        keyboardType="default" //para default nao obrigatorio.
                        secureTextEntry={true} //proteje a senha.
                        // ENVENTO PARA FAZERMOS ALGO ENQUANTO O TEXTO MUDA
                        onChangeText={senha => this.setState({ senha })}
                    />

                    <TouchableOpacity
                        style={styles.btnLogin}
                        onPress={this.realizarLogin}>
                        <Text style={styles.btnLoginText}>Login</Text>
                    </TouchableOpacity>
                </View>
            </ImageBackground>

        );
    }
}


const styles = StyleSheet.create({
    main: {
        flex: 1,
        //backgroundColor: '#F1F1F1', retirar pra nao ter conflito.
        justifyContent: 'center',
        alignItems: 'center',
        width: '100%',
        height: '100%',
    },

    mainImgLogin: { 
        //confirmar que sera branco
        height: 41, //altura
        width: 260, //largura img nao é quadrada
        margin: 60, //espacamento em todos os lados,menos pra cima.
        marginTop: 0, // tira espacamento pra cima
    },

    inputLogin: {
        height: 38,
        width: 240, //largura mesma do botao
        marginBottom: 40, //espacamento pra baixo
        fontSize: 18,
        color: '#FFF',
        borderColor: '#FFF', //linha separadora
        borderWidth: 1, //espessura.
        borderRadius: 50,
        fontSize: 14,
        padding: 10,
    },

    btnLoginText: {
        fontSize: 18, //aumentar um pouco
        fontFamily: 'Open Sans Light', //troca de fonte
        color: '#0094FF', //mesma cor identidade
        fontWeight: 'bold', //espacamento entre as letras
        textTransform: 'capitalize', //estilo maiusculo
    },

    btnLogin: {
        alignItems: 'center',
        justifyContent: 'center',
        height: 38,
        width: 150,
        backgroundColor: '#FFFFFF',
        borderColor: '#FFFFFF',
        borderWidth: 1,
        borderRadius: 50,
        shadowOffset: { height: 1, width: 1 },
    },
});