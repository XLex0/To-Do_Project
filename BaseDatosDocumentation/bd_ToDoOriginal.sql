PGDMP              
        |         
   Base_TO_DO    16.3    16.3 &               0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    41817 
   Base_TO_DO    DATABASE        CREATE DATABASE "Base_TO_DO" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE "Base_TO_DO";
                postgres    false            �            1259    41859 
   asignation    TABLE     �   CREATE TABLE public.asignation (
    idasignation integer NOT NULL,
    idlabel integer NOT NULL,
    idtask integer NOT NULL
);
    DROP TABLE public.asignation;
       public         heap    postgres    false            �            1259    41858    asignation_idasignation_seq    SEQUENCE     �   CREATE SEQUENCE public.asignation_idasignation_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE public.asignation_idasignation_seq;
       public          postgres    false    222                       0    0    asignation_idasignation_seq    SEQUENCE OWNED BY     [   ALTER SEQUENCE public.asignation_idasignation_seq OWNED BY public.asignation.idasignation;
          public          postgres    false    221            �            1259    41845    categorylabel    TABLE     �   CREATE TABLE public.categorylabel (
    idlabel integer NOT NULL,
    name character varying(100) NOT NULL,
    description text,
    iduser integer NOT NULL
);
 !   DROP TABLE public.categorylabel;
       public         heap    postgres    false            �            1259    41844    categorylabel_idlabel_seq    SEQUENCE     �   CREATE SEQUENCE public.categorylabel_idlabel_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.categorylabel_idlabel_seq;
       public          postgres    false    220                       0    0    categorylabel_idlabel_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.categorylabel_idlabel_seq OWNED BY public.categorylabel.idlabel;
          public          postgres    false    219            �            1259    41830    task    TABLE     u  CREATE TABLE public.task (
    idtask integer NOT NULL,
    description text NOT NULL,
    priority character varying(10),
    creationdate date NOT NULL,
    enddate date,
    iduser integer NOT NULL,
    CONSTRAINT task_priority_check CHECK (((priority)::text = ANY ((ARRAY['High'::character varying, 'Medium'::character varying, 'Low'::character varying])::text[])))
);
    DROP TABLE public.task;
       public         heap    postgres    false            �            1259    41829    task_idtask_seq    SEQUENCE     �   CREATE SEQUENCE public.task_idtask_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.task_idtask_seq;
       public          postgres    false    218                       0    0    task_idtask_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.task_idtask_seq OWNED BY public.task.idtask;
          public          postgres    false    217            �            1259    41819    usuario    TABLE     �   CREATE TABLE public.usuario (
    iduser integer NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(255) NOT NULL,
    email character varying(100) NOT NULL
);
    DROP TABLE public.usuario;
       public         heap    postgres    false            �            1259    41818    usuario_iduser_seq    SEQUENCE     �   CREATE SEQUENCE public.usuario_iduser_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.usuario_iduser_seq;
       public          postgres    false    216                       0    0    usuario_iduser_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.usuario_iduser_seq OWNED BY public.usuario.iduser;
          public          postgres    false    215            b           2604    41862    asignation idasignation    DEFAULT     �   ALTER TABLE ONLY public.asignation ALTER COLUMN idasignation SET DEFAULT nextval('public.asignation_idasignation_seq'::regclass);
 F   ALTER TABLE public.asignation ALTER COLUMN idasignation DROP DEFAULT;
       public          postgres    false    221    222    222            a           2604    41848    categorylabel idlabel    DEFAULT     ~   ALTER TABLE ONLY public.categorylabel ALTER COLUMN idlabel SET DEFAULT nextval('public.categorylabel_idlabel_seq'::regclass);
 D   ALTER TABLE public.categorylabel ALTER COLUMN idlabel DROP DEFAULT;
       public          postgres    false    219    220    220            `           2604    41833    task idtask    DEFAULT     j   ALTER TABLE ONLY public.task ALTER COLUMN idtask SET DEFAULT nextval('public.task_idtask_seq'::regclass);
 :   ALTER TABLE public.task ALTER COLUMN idtask DROP DEFAULT;
       public          postgres    false    218    217    218            _           2604    41822    usuario iduser    DEFAULT     p   ALTER TABLE ONLY public.usuario ALTER COLUMN iduser SET DEFAULT nextval('public.usuario_iduser_seq'::regclass);
 =   ALTER TABLE public.usuario ALTER COLUMN iduser DROP DEFAULT;
       public          postgres    false    216    215    216            
          0    41859 
   asignation 
   TABLE DATA           C   COPY public.asignation (idasignation, idlabel, idtask) FROM stdin;
    public          postgres    false    222   �+                 0    41845    categorylabel 
   TABLE DATA           K   COPY public.categorylabel (idlabel, name, description, iduser) FROM stdin;
    public          postgres    false    220   ,                 0    41830    task 
   TABLE DATA           \   COPY public.task (idtask, description, priority, creationdate, enddate, iduser) FROM stdin;
    public          postgres    false    218   m,                 0    41819    usuario 
   TABLE DATA           D   COPY public.usuario (iduser, username, password, email) FROM stdin;
    public          postgres    false    216   �,                  0    0    asignation_idasignation_seq    SEQUENCE SET     I   SELECT pg_catalog.setval('public.asignation_idasignation_seq', 3, true);
          public          postgres    false    221                       0    0    categorylabel_idlabel_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.categorylabel_idlabel_seq', 3, true);
          public          postgres    false    219                       0    0    task_idtask_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.task_idtask_seq', 3, true);
          public          postgres    false    217                       0    0    usuario_iduser_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.usuario_iduser_seq', 3, true);
          public          postgres    false    215            o           2606    41864    asignation asignation_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.asignation
    ADD CONSTRAINT asignation_pkey PRIMARY KEY (idasignation);
 D   ALTER TABLE ONLY public.asignation DROP CONSTRAINT asignation_pkey;
       public            postgres    false    222            m           2606    41852     categorylabel categorylabel_pkey 
   CONSTRAINT     c   ALTER TABLE ONLY public.categorylabel
    ADD CONSTRAINT categorylabel_pkey PRIMARY KEY (idlabel);
 J   ALTER TABLE ONLY public.categorylabel DROP CONSTRAINT categorylabel_pkey;
       public            postgres    false    220            k           2606    41838    task task_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_pkey PRIMARY KEY (idtask);
 8   ALTER TABLE ONLY public.task DROP CONSTRAINT task_pkey;
       public            postgres    false    218            e           2606    41828    usuario usuario_email_key 
   CONSTRAINT     U   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_email_key UNIQUE (email);
 C   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_email_key;
       public            postgres    false    216            g           2606    41824    usuario usuario_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (iduser);
 >   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_pkey;
       public            postgres    false    216            i           2606    41826    usuario usuario_username_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_username_key UNIQUE (username);
 F   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_username_key;
       public            postgres    false    216            r           2606    41865 "   asignation asignation_idlabel_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.asignation
    ADD CONSTRAINT asignation_idlabel_fkey FOREIGN KEY (idlabel) REFERENCES public.categorylabel(idlabel);
 L   ALTER TABLE ONLY public.asignation DROP CONSTRAINT asignation_idlabel_fkey;
       public          postgres    false    220    4717    222            s           2606    41870 !   asignation asignation_idtask_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.asignation
    ADD CONSTRAINT asignation_idtask_fkey FOREIGN KEY (idtask) REFERENCES public.task(idtask);
 K   ALTER TABLE ONLY public.asignation DROP CONSTRAINT asignation_idtask_fkey;
       public          postgres    false    4715    222    218            q           2606    41853 '   categorylabel categorylabel_iduser_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.categorylabel
    ADD CONSTRAINT categorylabel_iduser_fkey FOREIGN KEY (iduser) REFERENCES public.usuario(iduser);
 Q   ALTER TABLE ONLY public.categorylabel DROP CONSTRAINT categorylabel_iduser_fkey;
       public          postgres    false    220    4711    216            p           2606    41839    task task_iduser_fkey    FK CONSTRAINT     y   ALTER TABLE ONLY public.task
    ADD CONSTRAINT task_iduser_fkey FOREIGN KEY (iduser) REFERENCES public.usuario(iduser);
 ?   ALTER TABLE ONLY public.task DROP CONSTRAINT task_iduser_fkey;
       public          postgres    false    216    218    4711            
      x�3�4�4�2�B.cN ����� !��         C   x�3�t-�,,M-IT0�tI-N.�,H�<�9O!%U!!e�e�Pi�[���1B�1n�Ɯ�\1z\\\ �^+�         f   x�3�I,JMT0THI�Q(-.M,��W0���L��4202�54�54@0M9�����P4q���d����XF@���ڌQ�s��#�X ��rs��qqq �)(6         C   x�3�,-.M,��7�,H,..�/J1�9�V$���%��r����*B(*�a��p�!�1z\\\ �-�     