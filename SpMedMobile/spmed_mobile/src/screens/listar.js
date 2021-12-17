import React, { Component } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { FlatList, Image, ImageBackground, StyleSheet, Text, View } from 'react-native';

import api from '../services/api';

export default class Consultas extends Component {
    constructor(props) {
        super(props);
        this.state = {
            listaConsultas: [],
        };
    }

    buscarConsultas = async () => {

        const token = await AsyncStorage.getItem('userToken');

        console.warn('buscar consultas')

        console.warn(token)

        const resposta = await api.get('/consulta/listar/minhas', {
            headers: {
                Authorization: 'Bearer ' + token
            }

        })
        // console.warn(token)

        const dadosDaApi = await resposta.data.listaConsultas;
        console.warn(dadosDaApi)
        this.setState({ listaConsultas: dadosDaApi });
    };

    componentDidMount() {
        this.buscarConsultas();
        console.warn(this.state.listaConsultas)
    }
    render() {
        return (
            <View style={styles.main}>
                <ImageBackground
                    source={require('../../assets/back-lista.png')}
                    style={StyleSheet.absoluteFillObject}
                >
                    <View style={styles.mainHeaderPerfil}>
                        <ImageBackground
                            source={require('../../assets/back-perfil-listar.png')}
                            style={styles.luca}
                        >
                            <Text style={styles.mainHeaderText}>{'Minhas Consultas'.toUpperCase()}</Text>
                            <Image source={require('../../assets/Foto-Perfil.png')}
                                style={styles.image_perfil} />
                            <Text style={styles.welcomeHeaderText}>{'Seja Bem-Vindo, Dr.Padronis'.toUpperCase()}</Text>
                        </ImageBackground>
                    </View>
                    <View style={styles.mainBody}>
                        <FlatList
                            contentContainerStyle={styles.mainBodyContent}
                            data={this.state.listaConsultas}
                            keyExtractor={item => item.idConsulta}
                            renderItem={this.renderItem}
                        />
                    </View>
                </ImageBackground>
            </View >
        );
    }
    renderItem = ({ item }) => (
        // <Text style={{ fontSize: 20, color: 'red' }}>{item.nomeEvento}</Text>

        <View style={styles.flatItemRow}>
            {/* <Text> {item.idConsulta} </Text> */}
            <View style={styles.flatItemContainer}>
                <Text style={styles.flatItemTitle}> {'Doutor:'} {item.idMedicoNavigation.idUsuarioNavigation.nome} </Text>
                <Text style={styles.flatItemTitle}> {'Paciente:'} {item.idPacienteNavigation.idUsuarioNavigation.nome} </Text>

                <Text style={styles.flatItemTitle}>
                    {'Data: '}
                    {Intl.DateTimeFormat("pt-BR", {
                        month: 'numeric', day: 'numeric',
                    }).format(new Date(item.dataConsulta))}
                    <Text> - </Text>
                    {Intl.DateTimeFormat("pt-BR", {
                        hour: 'numeric', minute: 'numeric', hour12: false
                    }).format(new Date(item.dataConsulta))}
                </Text>
            </View>
            <View style={styles.flatDescriptionContainer}>
                <Text style={styles.flatItemTitle}> {'Descrição:'} </Text>
                <Text style={styles.flatItemInfo}> {item.descricao} </Text>
            </View>
        </View>
    );
}


const styles = StyleSheet.create({
    luca: {
        width: 411,
        height: 250,

        justifyContent: 'space-evenly',
        alignItems: 'center'

    },

    main: {
        flex: 1,
        // backgroundColor: '#F9F9F9',
        width: '100%',
        height: '100%',
    },
    mainHeaderPerfil: {
        borderRadius: 20,
        width: 411,
        height: 220,
        justifyContent: 'center',
        alignItems: 'center',
    },
    mainHeaderText: {
        marginTop: 15,
        fontSize: 18,
        fontFamily: 'Andada SC Bold',
        color: '#F2F2F2',
    },
    welcomeHeaderText: {
        color: '#FFF',
        fontWeight: '600',
    },
    image_perfil: {
        width: 100,
        height: 100,
    },

    mainBody: {
        flex: 1,
    },

    mainBodyContent: {
        flex: 1,
    },

    flatItemRow: {
        alignSelf: 'center',
        flexDirection: 'row',
        backgroundColor: '#0094FF',
        marginTop: 40,
        width: '85%',
        height: '52%',
        borderRadius: 20,

    },
    flatItemContainer: {
        flex: 1,
        justifyContent: 'space-evenly',
        marginLeft: 15,
    },
    flatItemTitle: {
        fontSize: 15,
        color: '#fff',
        fontWeight: '500',
        marginRight: 25,
    },
    flatItemInfo: {
        textAlign: 'center',
        fontWeight: '400',
        fontSize: 13,
        color: '#FFF',
        lineHeight: 24,
        marginRight: 25,
    },

    flatDescriptionContainer: {
        height: '50%',
        justifyContent: 'space-evenly',
        marginTop: 25,
    },  
    // backConsultas:  {
    //     borderRadius: 20
    // },  
});